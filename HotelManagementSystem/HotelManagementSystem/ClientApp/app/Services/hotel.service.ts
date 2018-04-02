import { Injectable, Inject } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class HotelService {
    myAppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.myAppUrl = baseUrl;
    }

    getRooms() {
        return this._http.get(this.myAppUrl + 'api/Room/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    filterRooms(filters: any) {
        return this._http.post(this.myAppUrl + 'api/Room/FilterRooms', filters)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    getRoomById(id: number) {  
        return this._http.get(this.myAppUrl + "api/Room/Details/" + id)  
            .map((response: Response) => response.json())  
            .catch(this.errorHandler)  
    }

    //getFilteredRooms(room: any) {
    //    return this._http.get(this.myAppUrl + 'api/Room/FilterRooms', room)
    //        .map((response: Response) => response.json())
    //        .catch(this.errorHandler);
    //}

    AddRoom(room: any) {
        return this._http.post(this.myAppUrl + 'api/Room/Create', room)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }

    DeleteRoom(id: number) {
        return this._http.delete(this.myAppUrl + "api/Room/Delete/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    getHotelDetailsbyLicenceNumber(id: string) {
        return this._http.get(this.myAppUrl + "api/Room/HotelDetails/" + id)
            .map((response: Response) => response.json())
            .catch(this.errorHandler)
    }  

    showBookedRooms() { return this._http.get(this.myAppUrl + 'api/Room/GetBookedRooms').map((response: Response) => response.json()).catch(this.errorHandler); }

    //updateBookedRoom(id: number) {
    //    return this._http.get(this.myAppUrl + "api/Room/UpdateBookedRoom/" + id)
    //        .map((response: Response) => response.json())
    //        .catch(this.errorHandler)
    //}

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}  