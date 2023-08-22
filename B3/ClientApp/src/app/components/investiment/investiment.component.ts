import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CdbService } from 'src/app/services/cdb.service';
import { CdbIvestiment } from 'src/app/shared/models/cdb-ivestiment.model';

@Component({
  selector: 'app-investiment',
  templateUrl: './investiment.component.html',
  styleUrls: ['./investiment.component.scss']
})
export class InvestimentComponent implements OnInit{
    constructor(public _cdbService: CdbService,) {  }
    public ivestiment: CdbIvestiment;
    public form: FormGroup;
    ngOnInit(): void {
       this.form = this.createFormGroup();
    }
    
    public createFormGroup(){
        return new FormGroup({
          initialInvestment: new FormControl('', [Validators.required, Validators.min(0)]),
          deadlineInMonths: new FormControl('', [Validators.required, Validators.min(1), Validators.pattern("^[0-9]*$")]),
        })
    }

    onSubmit(){
        if(this.form.valid){
            this._cdbService.calculateInvestment(this.form.value).subscribe(res => {
                this.ivestiment = res.data;
            })
        }
    }
}
