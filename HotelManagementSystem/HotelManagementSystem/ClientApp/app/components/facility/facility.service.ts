import { Injectable, Inject } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class FacilityService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getFacilities() {
        return this._http.get(this.myAppUrl + 'api/Facility/GetFacilities')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    
    DeleteFacility(id: number) {
        return this._http.delete(this.myAppUrl + "api/Facility/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    getFacilityNames() {
        return this._http.get(this.myAppUrl + 'api/Facility/GetFacilityNames')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }
    

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

}  