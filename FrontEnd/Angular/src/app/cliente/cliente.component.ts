import { ApiService } from "./../api.service";
import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { DataService } from "../data.service";
import { Cliente } from "../classes";
import Swal from "sweetalert2";

@Component({
  selector: "app-cliente",
  templateUrl: "./cliente.component.html",
  styleUrls: ["./cliente.component.css"]
})
export class ClienteComponent implements OnInit {
  clientes$: Cliente[] = [];
  title: string = "Clientes";

  constructor(
    private api: ApiService,
    private router: Router,
    private data: DataService
  ) {
    this.data.changeTitle(this.title);
    this.data.changeRoute("cliente");
    this.api.setRoute("cliente");
  }

  ngOnInit() {
    this.getClientes();
  }

  private getClientes() {
    this.api.apiGet().subscribe(data => {
      this.clientes$ = data.MultiData;
    });
  }

  newClient() {
    this.router.navigate(["cliente/cadastro"]);
  }

  viewClient(id: Number) {
    this.router.navigate(["cliente/" + String(id)]);
  }

  editClient(id: number) {
    this.router.navigate(["cliente/" + String(id) + "/edit"]);
  }

  removeClient(id: number) {
    this.api.apiRemove(id).subscribe(data => {
      this.router.navigateByUrl("", { skipLocationChange: true }).then(() => {
        Swal.fire("Sucesso!", data.Message, "success");
        this.router.navigate(["cliente"]);
      });
    });
  }
}
