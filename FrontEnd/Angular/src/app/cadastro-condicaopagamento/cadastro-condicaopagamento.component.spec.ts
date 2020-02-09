import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroCondicaopagamentoComponent } from './cadastro-condicaopagamento.component';

describe('CadastroCondicaopagamentoComponent', () => {
  let component: CadastroCondicaopagamentoComponent;
  let fixture: ComponentFixture<CadastroCondicaopagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastroCondicaopagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroCondicaopagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
