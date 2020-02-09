import { DataService } from "./../data.service";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpHeaders, HttpClient } from "@angular/common/http";

class NovaMensagem {
  public IdContato: number;
  public Descricao: string;
}

@Component({
  selector: "app-cadastro-mensagem",
  templateUrl: "./cadastro-mensagem.component.html",
  styleUrls: ["./cadastro-mensagem.component.css"]
})
export class CadastroMensagemComponent implements OnInit {
  form: FormGroup;

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  };

  title: string = "Nova Mensagem";

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private data: DataService
  ) {
    this.form = this.formBuilder.group({
      descricao: ["", Validators.required]
    });
    this.data.changeTitle(this.title);
  }

  ngOnInit() {}

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    let idContato = this.activatedRoute.snapshot.params["id"];

    let novaMensagem = this.form.value as NovaMensagem;
    novaMensagem.IdContato = idContato;

    this.http
      .post(
        "http://localhost:49493/api/mensagens/",
        JSON.stringify(novaMensagem),
        this.httpOptions
      )
      .subscribe(
        data => {
          this.router.navigate(["contatos/" + idContato + "/mensagens"]);
        },
        error => {
          console.log("Error", error);
        }
      );
  }
}
