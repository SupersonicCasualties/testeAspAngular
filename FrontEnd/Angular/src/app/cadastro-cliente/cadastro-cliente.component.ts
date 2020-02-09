import Swal from "sweetalert2";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Cliente } from "./../classes/cliente";
import { DataService } from "./../data.service";
import { Router, ActivatedRoute } from "@angular/router";
import { ApiService } from "./../api.service";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-cadastro-cliente",
  templateUrl: "./cadastro-cliente.component.html",
  styleUrls: ["./cadastro-cliente.component.css"]
})
export class CadastroClienteComponent implements OnInit {
  Cliente: Cliente;
  idCliente: number;
  form: FormGroup;
  title: string = "Novo Cliente";

  type: string = "new";
  isEdit: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private data: DataService
  ) {
    this.data.changeTitle(this.title);
    this.data.changeRoute("cliente");
    this.api.setRoute("cliente");

    this.form = this.formBuilder.group({
      Nome: ["", [Validators.required]],
      Email: ["", [Validators.required, Validators.email]],
      Cpf: "",
      Uf: "",
      Cidade: "",
      Bairro: "",
      Rua: "",
      Numero: ["", [Validators.pattern("^[0-9]*$")]],
      PontoReferencia: "",
      Complemento: "",
      Cep: ""
    });

    this.idCliente = this.activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    if (this.idCliente) {
      this.isEdit = this.activatedRoute.snapshot.routeConfig.path.includes(
        "edit"
      );
      this.viewOrEditCliente();
    }
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    const fnSuccess = data => {
      if (data.Code === 200) {
        Swal.fire("Sucesso", data.Message, "success");
        this.router.navigate(["cliente"]);
      } else {
        Swal.fire("Erro", data.Message, "error");
      }
    };

    const fnError = error => {
      console.log("Error", error);
    };

    let cliente = this.form.value as Cliente;

    if (this.isEdit) {
      this.api.apiUpdate(cliente, this.idCliente).subscribe(fnSuccess, fnError);
    } else {
      this.api.apiPost(cliente).subscribe(fnSuccess, fnError);
    }
  }

  back() {
    this.router.navigate(["cliente"]);
  }

  viewOrEditCliente() {
    this.api.apiGetById(this.idCliente).subscribe(data => {
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
      this.data.changeTitle("Visualizando Cliente");
    } else {
      this.type = "edit";
      this.data.changeTitle("Editando Cliente");
    }
  }
}
