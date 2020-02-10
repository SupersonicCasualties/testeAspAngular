import { Produto } from "./Produto";
import { Condicaopagamento } from "./Condicaopagamento";
import { Cliente } from "./cliente";

export class PedidoItems {
  public Produto: Produto;
  public ProdutoId: number;
  public ValorUnitario?: number;
  public ValorBruto?: number;
  public ValorLiquido?: number;
  public Desconto?: number;
  public Quantidade?: number;
}

export class Pedido {
  public Id: number;
  public ClienteId: number;
  public Cliente: Cliente;
  public ValorLiquido: number;
  public ValorBruto: number;
  public Desconto: number;
  public DataHora: any;
  public CondicaoPagamentoId: number;
  public CondicaoPagamento: Condicaopagamento;
  public PedidoItems: PedidoItems[];
}
