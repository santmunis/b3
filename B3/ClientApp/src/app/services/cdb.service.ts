import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BackendResponse, BackendService } from '../shared/services/backend.service';
import { CdbIvestiment } from '../shared/models/cdb-ivestiment.model';


@Injectable({providedIn: 'root'})
export class CdbService {
    private apiUrl = `${environment.apiUrl}/cdb/`;

    constructor(private httpClient: BackendService) {
    }

    calculateInvestment({deadlineInMonths, initialInvestment}: {initialInvestment: number, deadlineInMonths: number}): Observable<BackendResponse<CdbIvestiment>> {
        const url = `${this.apiUrl}investiment/calculate`;
        return this.httpClient.get(url, [
            {name: "initialInvestment", value: initialInvestment.toString() },
            {name: "deadlineInMonths", value: deadlineInMonths.toString() },
        ]);
    }
}