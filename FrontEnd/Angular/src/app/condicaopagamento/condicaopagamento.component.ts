import { Condicaopagamento } from "./../classes";
import { Component, OnInit } from "@angular/core";
import { ApiService } from "../api.service";
import { Router } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-condicaopagamento",
  templateUrl: "./condicaopagamento.component.html",
  styleUrls: ["./condicaopagamento.component.css"]
})
export class CondicaopagamentoComponent implements OnInit {
  condicoes$: Condicaopagamento[];
  title: string = "Condição de Pagamento";

  myRoute: string = "condicaopagamento";

  constructor(
    private api: ApiService,
    private router: Router,
    private data: DataService
  ) {
    this.data.changeTitle(this.title);
    this.data.changeRoute(this.myRoute);
    this.api.setRoute(this.myRoute);
  }

  ngOnInit() {
    this.getCondicoes();
  }

  private getCondicoes() {
    this.api.apiGet().subscribe(data => {
      this.condicoes$ = data.MultiData;
    });
  }

  newCondicao() {
    this.router.navigate([`${this.myRoute}/cadastro`]);
  }

  viewCondicao(id: Number) {
    this.router.navigate([`${this.myRoute}/${String(id)}`]);
  }

  editCondicao(id: number) {
    this.router.navigate([`${this.myRoute}/${String(id)}/edit/`]);
  }

  removeClient(id: number) {
    this.api.apiRemove(id).subscribe(data => {
      this.router.navigateByUrl("", { skipLocationChange: true }).then(() => {
        Swal.fire("Sucesso!", data.Message, "success");
        this.router.navigate([this.myRoute]);
      });
    });
  }
}
