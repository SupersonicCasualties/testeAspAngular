import { Produtocategoria } from "./../classes/Produtocategoria";
import { Produto } from "./../classes/Produto";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ApiService } from "../api.service";
import { Router, ActivatedRoute } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-cadastro-produto",
  templateUrl: "./cadastro-produto.component.html",
  styleUrls: ["./cadastro-produto.component.css"]
})
export class CadastroProdutoComponent implements OnInit {
  categorias$: Produtocategoria[];
  Produto: Produto;
  idProduto: number;
  form: FormGroup;
  title: string = "Novo Produto";

  type: string = "new";
  isEdit: boolean = false;

  myRoute: string = "produto";

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
      PrecoVenda: [
        "",
        Validators.pattern(/^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$/)
      ],
      Imagem: [null],
      CodigoBarras: {
        value: "",
        disabled: true
      },
      ProdutoCategoriaId: ["", [Validators.required]]
    });

    this.idProduto = this.activatedRoute.snapshot.params["id"];

    this.setCategorias();
  }

  ngOnInit() {
    if (this.idProduto) {
      this.isEdit = this.activatedRoute.snapshot.routeConfig.path.includes(
        "edit"
      );
      this.viewOrEditProduto();
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

    let produto = this.form.value as Produto;

    if (this.isEdit) {
      this.api.apiUpdate(produto, this.idProduto).subscribe(fnSuccess, fnError);
    } else {
      this.api.apiPost(produto).subscribe(fnSuccess, fnError);
    }
  }

  back() {
    this.router.navigate([this.myRoute]);
  }

  viewOrEditProduto() {
    this.api.apiGetById(this.idProduto).subscribe(data => {
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

        if (key == "ProdutoCategoriaId") {
          intObj = {
            [key]: data.ProdutoCategoria.Id
          };
        }

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
      this.data.changeTitle("Visualizando Produto");
    } else {
      this.type = "edit";
      this.data.changeTitle("Editando Produto");
    }
  }

  handleImageEvent(event) {
    let files: any[] = event.target.files;

    if (files.length <= 0) return;

    let file: File = files[0];

    if (file.size > 1000000) {
      Swal.fire("Erro!", "Arquivo maior que 2mb!", "error");
      return;
    }

    this.toBase64(file)
      .then(data => {
        this.form.patchValue({
          Imagem: data
        });
      })
      .catch(error => {
        this.form.setValue({
          Imagem: null
        });
        console.error(error);
      });
  }

  toBase64(file: File) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = error => reject(error);
    });
  }

  setCategorias() {
    this.api.apiGet("produtocategoria").subscribe(data => {
      if (data.Code == 200) {
        this.categorias$ = data.MultiData;
      }
    });
  }
}
