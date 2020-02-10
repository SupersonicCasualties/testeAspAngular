import { Component, OnInit } from "@angular/core";
import { Produto } from "../classes";
import { ApiService } from "../api.service";
import { Router } from "@angular/router";
import { DataService } from "../data.service";
import Swal from "sweetalert2";

@Component({
  selector: "app-produto",
  templateUrl: "./produto.component.html",
  styleUrls: ["./produto.component.css"]
})
export class ProdutoComponent implements OnInit {
  produtos$: Produto[];
  title: string = "Produtos";

  myRoute: string = "produto";

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
      console.log(data.MultiData);
      this.produtos$ = data.MultiData;
    });
  }

  newProduto() {
    this.router.navigate([`${this.myRoute}/cadastro`]);
  }

  viewProduto(id: Number) {
    this.router.navigate([`${this.myRoute}/${String(id)}`]);
  }

  editProduto(id: number) {
    this.router.navigate([`${this.myRoute}/${String(id)}/edit/`]);
  }

  removeProduto(id: number) {
    this.api.apiRemove(id).subscribe(data => {
      this.router.navigateByUrl("", { skipLocationChange: true }).then(() => {
        Swal.fire("Sucesso!", data.Message, "success");
        this.router.navigate([this.myRoute]);
      });
    });
  }
}
