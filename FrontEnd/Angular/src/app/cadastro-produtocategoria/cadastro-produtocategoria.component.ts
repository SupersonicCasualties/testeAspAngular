import { Produtocategoria } from "./../classes/Produtocategoria";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ApiService } from "../api.service";
import { Router, ActivatedRoute } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-cadastro-produtocategoria",
  templateUrl: "./cadastro-produtocategoria.component.html",
  styleUrls: ["./cadastro-produtocategoria.component.css"]
})
export class CadastroProdutocategoriaComponent implements OnInit {
  Cliente: Produtocategoria;
  idProdutoCategoria: number;
  form: FormGroup;
  title: string = "Nova Categoria de Produto";

  type: string = "new";
  isEdit: boolean = false;

  myRoute: string = "produtocategoria";

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
      Descricao: ["", [Validators.required]]
    });

    this.idProdutoCategoria = this.activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    if (this.idProdutoCategoria) {
      this.isEdit = this.activatedRoute.snapshot.routeConfig.path.includes(
        "edit"
      );
      this.viewOrEditCategoria();
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

    let cliente = this.form.value as Produtocategoria;

    if (this.isEdit) {
      this.api
        .apiUpdate(cliente, this.idProdutoCategoria)
        .subscribe(fnSuccess, fnError);
    } else {
      this.api.apiPost(cliente).subscribe(fnSuccess, fnError);
    }
  }

  back() {
    this.router.navigate([this.myRoute]);
  }

  viewOrEditCategoria() {
    this.api.apiGetById(this.idProdutoCategoria).subscribe(data => {
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
      this.data.changeTitle("Visualizando Categoria de Produto");
    } else {
      this.type = "edit";
      this.data.changeTitle("Editando Categoria de Produto");
    }
  }
}
