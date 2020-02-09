import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SweetAlert2Module } from "@toverux/ngx-sweetalert2";
import { NgxMaskModule, IConfig } from "ngx-mask";

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MaterialImports } from "./MaterialImports";

import { AppComponent } from "./app.component";
import { CadastroContatoComponent } from "./cadastro-contato/cadastro-contato.component";
import { ContatosComponent } from "./contatos/contatos.component";
import { MensagensComponent } from "./mensagens/mensagens.component";
import { CadastroMensagemComponent } from "./cadastro-mensagem/cadastro-mensagem.component";
import { ClienteComponent } from "./cliente/cliente.component";
import { CadastroClienteComponent } from "./cadastro-cliente/cadastro-cliente.component";
import { ProdutocategoriaComponent } from "./produtocategoria/produtocategoria.component";
import { CadastroProdutocategoriaComponent } from "./cadastro-produtocategoria/cadastro-produtocategoria.component";
import { CondicaopagamentoComponent } from "./condicaopagamento/condicaopagamento.component";
import { CadastroCondicaopagamentoComponent } from "./cadastro-condicaopagamento/cadastro-condicaopagamento.component";

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};

// Problema no pacote do SweetAlert 2
// Lancando erros no cmd
const appRoutes: Routes = [
  { path: "contatos", component: ContatosComponent },
  { path: "contatos/cadastro", component: CadastroContatoComponent },
  { path: "contatos/:id/mensagens", component: MensagensComponent },
  {
    path: "contatos/:id/mensagens/cadastro",
    component: CadastroMensagemComponent
  },

  // Cliente
  { path: "cliente", component: ClienteComponent },
  { path: "cliente/cadastro", component: CadastroClienteComponent },
  { path: "cliente/:id", component: CadastroClienteComponent },
  { path: "cliente/:id/edit", component: CadastroClienteComponent },

  // Cliente
  { path: "produtocategoria", component: ProdutocategoriaComponent },
  {
    path: "produtocategoria/cadastro",
    component: CadastroProdutocategoriaComponent
  },
  {
    path: "produtocategoria/:id",
    component: CadastroProdutocategoriaComponent
  },
  {
    path: "produtocategoria/:id/edit",
    component: CadastroProdutocategoriaComponent
  },

  // Cliente
  { path: "condicaopagamento", component: CondicaopagamentoComponent },
  {
    path: "condicaopagamento/cadastro",
    component: CadastroCondicaopagamentoComponent
  },
  {
    path: "condicaopagamento/:id",
    component: CadastroCondicaopagamentoComponent
  },
  {
    path: "condicaopagamento/:id/edit",
    component: CadastroCondicaopagamentoComponent
  },

  // criar uma home
  { path: "", redirectTo: "/contatos", pathMatch: "full" }
];

@NgModule({
  declarations: [
    AppComponent,
    CadastroContatoComponent,
    ContatosComponent,
    MensagensComponent,
    CadastroMensagemComponent,
    ClienteComponent,
    CadastroClienteComponent,
    ProdutocategoriaComponent,
    CadastroProdutocategoriaComponent,
    CondicaopagamentoComponent,
    CadastroCondicaopagamentoComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialImports,
    FormsModule,
    ReactiveFormsModule,
    SweetAlert2Module.forRoot(),
    NgxMaskModule.forRoot(options)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
