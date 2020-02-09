import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroProdutocategoriaComponent } from './cadastro-produtocategoria.component';

describe('CadastroProdutocategoriaComponent', () => {
  let component: CadastroProdutocategoriaComponent;
  let fixture: ComponentFixture<CadastroProdutocategoriaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastroProdutocategoriaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroProdutocategoriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
