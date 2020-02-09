import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutocategoriaComponent } from './produtocategoria.component';

describe('ProdutocategoriaComponent', () => {
  let component: ProdutocategoriaComponent;
  let fixture: ComponentFixture<ProdutocategoriaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProdutocategoriaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProdutocategoriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
