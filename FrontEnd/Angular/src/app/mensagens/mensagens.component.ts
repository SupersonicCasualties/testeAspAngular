import { DataService } from "./../data.service";
import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";

class Mensagem {
  public Descricao: string;
  public DataHora: Date;
}

class Contato {
  public Nome: string;
}

@Component({
  selector: "app-mensagens",
  templateUrl: "./mensagens.component.html",
  styleUrls: ["./mensagens.component.css"]
})
export class MensagensComponent implements OnInit {
  mensagens: Mensagem[] = [];

  contato: Contato = new Contato();

  title: string = "Mensagens de ";

  idContato: number;

  constructor(
    private http: HttpClient,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private data: DataService
  ) {
    this.idContato = this.activatedRoute.snapshot.params["id"];
  }

  ngOnInit() {
    this.http
      .get<Contato>("http://localhost:49493/api/contatos/" + this.idContato)
      .subscribe(contato => {
        this.contato = contato;

        this.data.changeTitle(this.title + contato.Nome);
      });

    this.http
      .get<Mensagem[]>(
        "http://localhost:49493/api/contatos/" + this.idContato + "/mensagens"
      )
      .subscribe(x => (this.mensagens = x));
  }

  novaMensagem() {
    this.router.navigate([
      "contatos/" + this.idContato + "/mensagens/cadastro"
    ]);
  }

  contatos() {
    this.router.navigate(["contatos"]);
  }
}
