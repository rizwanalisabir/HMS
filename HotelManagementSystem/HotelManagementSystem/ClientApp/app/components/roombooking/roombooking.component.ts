import { Component, Input, Inject, ViewChild, ElementRef } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { HotelService } from '../../Services/hotel.service'
import { Router } from '@angular/router';
import { FacilityService } from '../facility/facility.service';

@Component({
    selector: 'roombooking',
    templateUrl: './roombooking.component.html',
    providers: [HotelService]
})
export class RoomBookingComponent {
    public CheckInDate: Date;
    public CheckOutDate: Date;
    public paymentType: string[] = ['Paid', 'Pending', 'Partial'];
    public NoOfBed: number;
    public NoOfAdult: number;
    public NoOfChild: number;
    public PriceRange: any;
    public RoomType: string = "All";
    public Internet: boolean;
    public Tv: boolean;
    public Fridge: boolean;
    public bseUrl: string = "";
    public filterroomslist: any = {};

    //Facilites
    public facilities: IFacilities[];
    public facilityid: string = "0";
    public facilityname: string = "";
    public isavailable: boolean = false;
    public btnFacilitytext: string = "Add";
    public AddFacilityForm: boolean = false;
    public facilitiesComma: string = " ";

    constructor(public http: Http, private _router: Router, private _hotelService: HotelService, private _facilityService: FacilityService) {
        this.getFacilities();
    }

    RoomSearch(Checkin: string, Checkout: string, Noofbed: number, Noofadult: number, Noofchild: number) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        this.http.post(this.bseUrl + 'api/Room/FilterRoomsbyCustomer', JSON.stringify({ CheckInDate: Checkin, CheckOutDate: Checkout, NoOfBeds: Noofbed, NoOfAdults: Noofadult, NoOfChild: Noofchild, RoomType: this.RoomType, RoomFacilities: this.facilitiesComma}),
            { headers: headers }).map((response: Response) => response.json()).subscribe(
                data => {
                    this.filterroomslist = data;

                }, error => {
                }
            );
    }

    getFacilities() {
        this._facilityService.getFacilities().subscribe(
            data => this.facilities = data
        )
    }

    SetFacility(flag: boolean, name: string) {
        if (flag) {
            this.facilitiesComma += name + ",";
        }
        else {
            this.facilitiesComma = this.removeValue(this.facilitiesComma, name);
        }
    }

    removeValue(list: any, value: any) {
        return list.replace(new RegExp(",?" + value + ",?"), function (match: any) {
            var first_comma = match.charAt(0) === ',',
                second_comma;
            if (first_comma &&
                (second_comma = match.charAt(match.length - 1) === ',')) {
                return ',';
            }
            return '';
        });
    };
}

interface IFacilities {
    facilityid: number;
    facilityname: string;
    isavailable: boolean;
}