import { Produtocategoria } from ".";

export class Produto {
  public Descricao: string;
  public PrecoVenda: number;
  public Imagem: string;
  public CodigoBarras: number;
  public ProdutoCategoriaId: number;
  public ProdutoCategoria?: Produtocategoria;
}
