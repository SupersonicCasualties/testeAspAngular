import { Condicaopagamento } from "./../classes";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ApiService } from "../api.service";
import { Router, ActivatedRoute } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-cadastro-condicaopagamento",
  templateUrl: "./cadastro-condicaopagamento.component.html",
  styleUrls: ["./cadastro-condicaopagamento.component.css"]
})
export class CadastroCondicaopagamentoComponent implements OnInit {
  Cliente: Condicaopagamento;
  idCondicaoPagamento: number;
  form: FormGroup;
  title: string = "Nova Condição de Pagamento";

  type: string = "new";
  isEdit: boolean = false;

  myRoute: string = "condicaopagamento";
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
      Descricao: ["", [Validators.required]],
      Tipo: ["", [Validators.required]]
    });

    this.idCondicaoPagamento = this.activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    if (this.idCondicaoPagamento) {
      this.isEdit = this.activatedRoute.snapshot.routeConfig.path.includes(
        "edit"
      );
      this.viewOrEditCondicao();
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

    let condicao = this.form.value as Condicaopagamento;

    if (this.isEdit) {
      this.api
        .apiUpdate(condicao, this.idCondicaoPagamento)
        .subscribe(fnSuccess, fnError);
    } else {
      this.api.apiPost(condicao).subscribe(fnSuccess, fnError);
    }
  }

  back() {
    this.router.navigate([this.myRoute]);
  }

  viewOrEditCondicao() {
    this.api.apiGetById(this.idCondicaoPagamento).subscribe(data => {
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
    this.form.setValue(valueObj);

    if (!this.isEdit) {
      this.form.disable();
      this.type = "view";
      this.data.changeTitle("Visualizando Condição de Pagamento");
    } else {
      this.type = "edit";
      this.data.changeTitle("Editando Condição de Pagamento");
    }
  }
}
