import { Condicaopagamento } from "./../classes/Condicaopagamento";
import { Cliente, Pedido, Produto, PedidoItems } from "./../classes";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ApiService } from "../api.service";
import { Router, ActivatedRoute } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-cadastro-pedido",
  templateUrl: "./cadastro-pedido.component.html",
  styleUrls: ["./cadastro-pedido.component.css"]
})
export class CadastroPedidoComponent implements OnInit {
  clientes$: Cliente[];
  produtos$: Produto[];
  condicoes$: Condicaopagamento[];

  listProdutos: PedidoItems[] = [];
  Produto: Pedido;
  currentProduto: Produto;
  idPedido: number;
  form: FormGroup;
  title: string = "Novo Pedido";

  type: string = "new";
  isEdit: boolean = false;

  myRoute: string = "pedido";
  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private data: DataService
  ) {
    this.data.changeTitle(this.title);
    this.data.changeRoute(this.myRoute);
    this.api.setRoute(this.myRoute);

    this.form = this.formBuilder.group({
      ClienteId: ["", [Validators.required]],
      CondicaoPagamentoId: ["", [Validators.required]],
      ValorLiquido: [""],
      ProdutoId: [""],
      ValorProduto: [
        "",
        Validators.pattern(/^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$/)
      ],
      QuantidadeProduto: ["", Validators.pattern(/^[0-9]/)],
      DescontoProduto: [
        "",
        Validators.pattern(/^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$/)
      ]
    });

    this.idPedido = this.activatedRoute.snapshot.params["id"];

    this.setExtras();
  }

  ngOnInit() {
    if (this.idPedido) {
      this.isEdit = this.activatedRoute.snapshot.routeConfig.path.includes(
        "edit"
      );
      this.viewOrEditPedido();
    }
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    const fnSuccess = data => {
      if (data.Code === 200) {
        Swal.fire("Sucesso", data.Message, "success");
        this.router.navigate([this.myRoute]);
      } else {
        Swal.fire("Erro", data.Message, "error");
      }
    };

    const fnError = error => {
      console.log("Error", error);
    };

    let pedido = this.form.value as Pedido;
    pedido.PedidoItems = this.listProdutos;

    if (this.isEdit) {
      this.api.apiUpdate(pedido, this.idPedido).subscribe(fnSuccess, fnError);
    } else {
      this.api.apiPost(pedido).subscribe(fnSuccess, fnError);
    }
  }

  back() {
    this.router.navigate([this.myRoute]);
  }

  viewOrEditPedido() {
    this.api.apiGetById(this.idPedido).subscribe(data => {
      if (data.Code == 200) {
        this.setData(data.Data);
      }
    });
  }

  setData(data: any) {
    let valueObj = {};
    for (const key in data) {
      if (this.form.get(key)) {
        let intObj = {
          [key]: data[key]
        };
        valueObj = {
          ...valueObj,
          ...intObj
        };
      }
    }
    valueObj = {
      ...valueObj,
      ProdutoId: "",
      ValorProduto: "",
      QuantidadeProduto: "",
      DescontoProduto: ""
    };
    this.form.setValue(valueObj);

    this.listProdutos = data.PedidoItems;

    if (!this.isEdit) {
      this.form.disable();
      this.type = "view";
      this.data.changeTitle("Visualizando Produto");
    } else {
      this.type = "edit";
      this.data.changeTitle("Editando Produto");
    }
  }

  setExtras() {
    this.api.apiGet("condicaopagamento").subscribe(data => {
      if (data.Code == 200) {
        this.condicoes$ = data.MultiData;
      }
    });
    this.api.apiGet("cliente").subscribe(data => {
      if (data.Code == 200) {
        this.clientes$ = data.MultiData;
      }
    });
    this.api.apiGet("produto").subscribe(data => {
      if (data.Code == 200) {
        this.produtos$ = data.MultiData;
      }
    });
  }

  eventProdutoChanged(id: number) {
    if (!id) return;

    this.currentProduto = this.produtos$.find(p => p.Id == id);

    this.form.patchValue({
      ValorProduto: this.currentProduto.PrecoVenda
    });
  }

  addProduto(e) {
    e.preventDefault();
    let quantidade = parseInt(this.form.get("QuantidadeProduto").value);

    if (!this.currentProduto) {
      Swal.fire("Atenção", "Informe o produto!", "info");
      return;
    }

    if (!quantidade) {
      Swal.fire("Atenção", "Informe a quantidade!", "info");
      return;
    }

    let exists = this.findExistance();

    if (exists) {
      Swal.fire("Atenção", "Já o produto já existe na tabela!", "warning");
      return;
    }

    let desconto = this.form.get("DescontoProduto").value;
    desconto = desconto ? parseFloat(desconto) : 0;
    let produto = this.currentProduto;
    let item = new PedidoItems();

    item.ProdutoId = produto.Id;
    item.Produto = produto;
    item.Quantidade = quantidade;
    item.ValorBruto = parseFloat(this.formataDecimal(quantidade * produto.PrecoVenda));
    item.ValorLiquido = item.ValorBruto - desconto;
    item.Desconto = desconto;

    this.listProdutos.push(item);

    this.form.patchValue({
      ProdutoId: "",
      QuantidadeProduto: "",
      DescontoProduto: "",
      ValorProduto: ""
    });
    this.currentProduto = new Produto();
  }

  findExistance() {
    let produto = this.currentProduto;

    return this.listProdutos.filter(p => p.Produto.Id == produto.Id).length > 0;
  }

  removerProduto(e, id: number) {
    e.preventDefault();

    if (!id) return;

    this.listProdutos = this.listProdutos.filter(p => p.Produto.Id != id);
  }



/**
 * Arredonda valores e mostra no padrao BR
 * @param { number } valor Valor a ser arredondado
 * @param { integer } decimais Numero de casas decimais
 */
formataDecimal(valor, decimais = 2) {
  var n = valor;
  var c = isNaN((decimais = Math.abs(decimais))) ? 2 : decimais;
  var d = ".";
  var t = ",";
  var s = n < 0 ? "-" : "";
  var i: any = parseInt((n = Math.abs(+n || 0).toFixed(c))) + "";
  var j = (j = i.length) > 3 ? j % 3 : 0;
  return (
      s +
      (j ? i.substr(0, j) + t : "") +
      i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) +
      (c
          ? d +
            Math.abs(n - i)
                .toFixed(c)
                .slice(2)
          : "")
  );
}
}
