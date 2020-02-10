import { Produtocategoria } from ".";

export class Produto {
  public Id: number;
  public Descricao: string;
  public PrecoVenda: number;
  public Imagem: string;
  public CodigoBarras: number;
  public ProdutoCategoriaId: number;
  public ProdutoCategoria?: Produtocategoria;
}
