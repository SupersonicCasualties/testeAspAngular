import { Component, OnInit } from "@angular/core";
import { ApiService } from "../api.service";
import { Router } from "@angular/router";
import { DataService } from "../data.service";
import { Produtocategoria } from "../classes";
import Swal from "sweetalert2";

@Component({
  selector: "app-produtocategoria",
  templateUrl: "./produtocategoria.component.html",
  styleUrls: ["./produtocategoria.component.css"]
})
export class ProdutocategoriaComponent implements OnInit {
  categorias$: Produtocategoria[];
  title: string = "Categoria de Produtos";

  myRoute: string = "produtocategoria";

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
    this.getCategorias();
  }

  private getCategorias() {
    this.api.apiGet().subscribe(data => {
      this.categorias$ = data.MultiData;
    });
  }

  newCategoria() {
    this.router.navigate([`${this.myRoute}/cadastro`]);
  }

  viewCategoria(id: Number) {
    this.router.navigate([`${this.myRoute}/${String(id)}`]);
  }

  editCategoria(id: number) {
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
