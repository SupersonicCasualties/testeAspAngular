import { DataService } from "./../data.service";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { HttpHeaders, HttpClient } from "@angular/common/http";

class Contato {
  public Id: number;
  public Nome: string;
}

@Component({
  selector: "app-contatos",
  templateUrl: "./contatos.component.html",
  styleUrls: ["./contatos.component.css"]
})
export class ContatosComponent implements OnInit {
  contatos: Contato[] = [];

  title: string = "Contatos";

  httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private data: DataService
  ) {
    this.data.changeTitle(this.title);
    this.data.changeRoute("contatos");
  }

  ngOnInit() {
    this.http
      .get<Contato[]>("http://localhost:49493/api/contatos/")
      .subscribe(x => {
        this.contatos = x;
      });
  }

  novo() {
    this.router.navigate(["contatos/cadastro"]);
  }

  mensagens(contato: Contato) {
    this.router.navigate(["contatos/" + contato.Id + "/mensagens"]);
  }
}
