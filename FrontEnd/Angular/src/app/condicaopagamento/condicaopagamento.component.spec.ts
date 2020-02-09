import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CondicaopagamentoComponent } from './condicaopagamento.component';

describe('CondicaopagamentoComponent', () => {
  let component: CondicaopagamentoComponent;
  let fixture: ComponentFixture<CondicaopagamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CondicaopagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CondicaopagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
