import { Pedido } from "./../classes/Pedido";
import { Component, OnInit } from "@angular/core";
import Swal from "sweetalert2";
import { ApiService } from "../api.service";
import { Router } from "@angular/router";
import { DataService } from "../data.service";

@Component({
  selector: "app-pedido",
  templateUrl: "./pedido.component.html",
  styleUrls: ["./pedido.component.css"]
})
export class PedidoComponent implements OnInit {
  pedidos$: Pedido[];
  title: string = "Pedidos";

  myRoute: string = "pedido";
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
    this.getPedidos();
  }

  private getPedidos() {
    this.api.apiGet().subscribe(data => {
      this.pedidos$ = data.MultiData;
    });
  }

  newPedido() {
    this.router.navigate([`${this.myRoute}/cadastro`]);
  }

  viewPedido(id: number) {
    this.router.navigate([`${this.myRoute}/${String(id)}`]);
  }

  editPedido(id: number) {
    this.router.navigate([`${this.myRoute}/${String(id)}/edit/`]);
  }

  removePedido(id: number) {
    this.api.apiRemove(id).subscribe(data => {
      this.router.navigateByUrl("", { skipLocationChange: true }).then(() => {
        Swal.fire("Sucesso!", data.Message, "success");
        this.router.navigate([this.myRoute]);
      });
    });
  }
}
