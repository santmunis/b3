import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InvestimentComponent } from './investiment.component';
import { CdbService } from 'src/app/services/cdb.service';
import { of } from 'rxjs';
import { DEFAULT_CURRENCY_CODE, LOCALE_ID } from '@angular/core';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { InvestimentModule } from './investiment.module';

describe('InvestimentComponent', () => {
  let component: InvestimentComponent;
  let fixture: ComponentFixture<InvestimentComponent>;
  let cdbServiceStub: Partial<CdbService>; // Stub para o serviço

  cdbServiceStub = {
    calculateInvestment: jasmine.createSpy('calculateInvestment').and.returnValue(of({
      data: {
        grossValue: 1000,
        netValue: 900,
        tax: "25.00%"
      }
    }))
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // declarations: [InvestimentComponent],
      imports: [InvestimentModule,BrowserAnimationsModule],
      providers: [
        { provide: CdbService, useValue: cdbServiceStub },
        { provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL'}]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InvestimentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

   it('should not accept decimal values in the deadlineInMonths input', async () => {
  
    const inputElement = fixture.nativeElement.querySelector('input[formControlName="deadlineInMonths"]');
    inputElement.dispatchEvent(new Event('focus'));
    
    inputElement.value = '10.5';
    inputElement.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    
    
    inputElement.dispatchEvent(new Event('blur'));
    fixture.detectChanges();


    let errorMessage = fixture.nativeElement.querySelector("#deadlineInMonths");
    expect(errorMessage.textContent).toContain('O tempo de duração precisa ser um número inteiro de meses');
    
    inputElement.dispatchEvent(new Event('focus'));
    inputElement.value = '1000';
    inputElement.dispatchEvent(new Event('input'));
    inputElement.dispatchEvent(new Event('blur'));
    fixture.detectChanges();
 
    errorMessage = fixture.nativeElement.querySelector('#deadlineInMonths');
    expect(errorMessage).toBeFalsy();
  });

  it('should display calculated values when form is submitted', () => {
    const initialInvestmentInput = fixture.nativeElement.querySelector('input[formControlName="initialInvestment"]');
    const deadlineInMonthsInput = fixture.nativeElement.querySelector('input[formControlName="deadlineInMonths"]');
    const simulateButton = fixture.nativeElement.querySelector('.btn-calc');
    initialInvestmentInput.value = '1000';
    initialInvestmentInput.dispatchEvent(new Event('input'));
    deadlineInMonthsInput.value = '12';
    deadlineInMonthsInput.dispatchEvent(new Event('input'));

    simulateButton.click();
    fixture.detectChanges();

    const investmentDetails = fixture.nativeElement.querySelector('.ivestiment');


    var formattedText = investmentDetails.textContent.replace(/\s+/g, '').toLowerCase();

    expect(investmentDetails).toBeTruthy();
    expect(formattedText).toContain('Valor Bruto:  R$1,000.00'.replace(/\s+/g, '').toLowerCase());
    expect(formattedText).toContain('Valor Líquido: R$900.00'.replace(/\s+/g, '').toLowerCase());
    expect(formattedText).toContain('Imposto incedido: 25.00%'.replace(/\s+/g, '').toLowerCase());
  });
});