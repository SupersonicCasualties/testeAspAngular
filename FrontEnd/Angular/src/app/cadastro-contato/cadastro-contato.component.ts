import { DataService } from "./../data.service";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { HttpHeaders, HttpClient } from "@angular/common/http";

class NovoContato {
  public Nome: string;
}

@Component({
  selector: "app-cadastro-contato",
  templateUrl: "./cadastro-contato.component.html",
  styleUrls: ["./cadastro-contato.component.css"]
})
export class CadastroContatoComponent implements OnInit {
  form: FormGroup;

  title: string = "Novo Contato";

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  };

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private data: DataService
  ) {
    this.form = this.formBuilder.group({
      nome: ["", Validators.required]
    });

    this.data.changeTitle(this.title);
  }

  ngOnInit() {}

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    let novoContato = this.form.value as NovoContato;

    this.http
      .post(
        "http://localhost:49493/api/contatos/",
        JSON.stringify(novoContato),
        this.httpOptions
      )
      .subscribe(
        data => {
          this.router.navigate(["contatos"]);
        },
        error => {
          console.log("Error", error);
        }
      );
  }
}
