import { Component, Input, Inject, ViewChild, ElementRef } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { HotelService } from '../../Services/hotel.service'
import { Router } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { Observable } from 'rxjs/Observable';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FacilityComponent } from '../facility/facility.component';
import { FacilityService } from '../facility/facility.service';
import { Injectable } from '@angular/core';
import { NumberPickerComponent } from '../angular2-number-picker/angular2-number-picker.component';

@Component({
    selector: 'mainpage',
    templateUrl: './mainpage.component.html',
    providers: [HotelService, FacilityService]
})

@Injectable()
export class MainPageComponent {
    public roomType: string[] = ['All', 'Executive', 'Luxury', 'Business', 'Single bed', 'Double bed', 'Private', 'VIP'];
    public paymentType: string[] = ['Paid', 'Pending', 'Partial'];
    public RoomIdForBooking: number;
    public FeeCal: number;
    public FeeCalFix: number;
    public floorfilter : string;
    public roomnumberfilter : string;
    public Customername: string;
    public DateFrom: string = "";
    public DateTo: string = "";
    public PType: string;
    public facilitiesToEdit: string[];
    public facilities: IFacilities[];
    public facilitiesComma: string = " ";
    public roomslist: RoomData[];
    public filterroomslist: any = {};
    public hoteldetails: any = {};
    public bookedrooms: BookedRoom[];
    public roomid: number = 0;
    public numberofbeds: any = 2;
    public numberofadults: any = 2;
    public numberofchild: any = 2;
    public floor: string = "Basement";
    public Floor: number;
    public roomnumber: number = 4;
    public roomtype: string = "Single";
    public description: string = "";
    public btntext: string = "Add";
    public bseUrl: string = "";
    public feeperday: string = "0.00";
    public feeperweek: string = "0.00";
    public feepermonth: string = "0.00";
    public percentagechange: number = 0;
    public image: string = "";
    public status: boolean;
    public Option: string;
    public Internet: boolean;
    public Wifi: boolean;
    public Tv: boolean;
    public Fridge: boolean;
    public Microwave: boolean;
    public RoomType: string;
    public statustemp: string;

    AddTable: Boolean = false;
    public moreinfo: string = "no description was there";
    public fd: any;

    CancelBookedRoom(RoomId: number) {
        var ans = confirm("Do you want to unbook this Room?");
        if (ans) {
            var headers = new Headers();
            headers.append('Content-Type', 'application/json; charset=utf-8');
            this.http.put(this.bseUrl + 'api/Room/UpdateBookedRoom/' + RoomId,
                { headers: headers }).subscribe(
                response => {
                    this.showBookedRooms();

                }, error => {
                }
                );
        }
    }


    SetRoomType(value: any) {
        this.RoomType = value;
    }
    SetPaymentType(value: any) {
        this.PType = value;
    }

    Numberofbeds($event: any) {
        this.numberofbeds = $event;
    }

    Numberofadults($event: any) {
        this.numberofadults = $event;
    }

    Numberofchild($event: any) {
        this.numberofchild = $event;
    }
    


    ShowNewRoom() {
        this.AddTable = true;
        this.facilitiesComma = "";
        this.btntext = "Add";
        this.roomid = 0;
        this.numberofbeds = 2;
        this.numberofadults = 2;
        this.numberofchild = 2;
        this.floor = "Basement";
        this.roomnumber = 4;
        this.roomtype = "Single";
        this.description = "";
        this.feeperday = "0.00";
        this.feeperweek = "0.00";
        this.feepermonth = "0.00";
        this.bseUrl = "";
    }

    HideNewRoom() {
        this.AddTable = false;
    }

    constructor(public http: Http, private _router: Router, private _hotelService: HotelService, private _facilityService: FacilityService) {
        this.getRooms();
        this.getFacilities();
    }

    getRooms() {
        this._hotelService.getRooms().subscribe(
            data => this.roomslist = data
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

    getFacilities() {
        this._facilityService.getFacilities().subscribe(
            data => this.facilities = data
        )
    }

    showBookedRooms() {
        this._hotelService.showBookedRooms().subscribe(
            data => this.bookedrooms = data
        )
    }

    getHotelDetails() {
        this._hotelService.getHotelDetailsbyLicenceNumber("123-45").subscribe(
            data => this.hoteldetails = data
        )
    }

    delete(roomid: number) {
        var ans = confirm("Do you want to delete this Room with Id: " + roomid);
        if (ans) {
            this._hotelService.DeleteRoom(roomid).subscribe((data) => {
                this.getRooms();
            }, error => console.error(error))
        }
    }

    SetFrom(event: any) {
        this.DateFrom = event;
        if (this.DateTo != "") {
        this.SetTotalAmount();
        }
    }
    SetTo(event: any) {
        this.DateTo = event;
        if (this.DateFrom != "") {
            this.SetTotalAmount();
        }
    }
    SetTotalAmount() {
        var eventStartTime = new Date(this.DateFrom);
        var oneDay = 24 * 60 * 60 * 1000;
        var eventEndTime = new Date(this.DateTo);
        var duration = (eventEndTime.valueOf() - eventStartTime.valueOf()) / (oneDay);
        this.FeeCal = this.FeeCalFix * duration;
    }

    showinfo(desc: string) {
        if (desc != "") {
            this.moreinfo = desc;
        }
        else {
            this.moreinfo = "No further information was there...";
        }
    }

    onFileChange(event: any) {
        let fi = event.srcElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];

            let formData: FormData = new FormData();
            formData.append(fileToUpload.name, fileToUpload);
            let headers = new Headers();
            headers.append('Accept', 'application/json');
            // DON'T SET THE Content-Type to multipart/form-data, You'll get the Missing content-type boundary error
            let options = new RequestOptions({ headers: headers });

            this.http.post("api/Room/Test", formData, options)
                .subscribe(r => console.log(r));
        }
    }

    AddRoom(RoomId: string, Roomnumber: string, Floor: string,  Roomtype: string, Description: string, Feeperday: any | null, Feeperweek: any | null, Feepermonth: any | null, ChangePercentage: any, Status: boolean | any) {

        let formData: FormData = new FormData();
        if (this.fileList != null) {
            let fileList: FileList = this.fileList;
            if (fileList.length > 0) {
                let file: File = fileList[0];
                formData.append('uploadFile', file, file.name);
            }
        }
        formData.append('RoomID', RoomId.toString());
        formData.append('FeePerDay', Feeperday);
        formData.append('FeePerWeek', Feeperweek);
        formData.append('FeePerMonth', Feepermonth);
        formData.append('PercentageChange', ChangePercentage);
        formData.append('Floor', Floor);
        formData.append('RoomNo', Roomnumber);
        formData.append('NoOfBed', this.numberofbeds.toString());
        formData.append('NoOfAdult', this.numberofadults.toString());
        formData.append('NoOfChild', this.numberofchild.toString());
        formData.append('RoomType', Roomtype);
        if (Status) {
            formData.append('Status', 'Available');
        }
        else {
            formData.append('Status', 'Booked');
        }
        formData.append('Description', Description);
        formData.append('Facilities', this.facilitiesComma);
        let headers = new Headers();
        let options = new RequestOptions({ headers: headers });
        if (RoomId == "0") {
            this.http.post(this.bseUrl + 'api/Room/Create', formData, options)
                .map(res => res.json())
                .subscribe
                (
                data => {
                    if (data == true) {
                        this.getRooms();
                        this.HideNewRoom();
                        alert("Success: Room was saved...");
                    }
                    else {
                        alert("Error: There was some error while adding new room please try again...");
                    }
                },
                error => {
                    if (error) {
                        alert("Error: There was some error while adding new room please try again...");
                    }
                })
        }
        else {
            this.http.put(this.bseUrl + 'api/Room/Edit', formData, options)
                .map(res => res.json())
                .subscribe
                (
                data => {
                    if (data == true) {
                        this.getRooms();
                        this.HideNewRoom();
                        alert("Success: Room was updated...");
                    }
                    else {
                        alert("Error: There was some error while updating room please try again...");
                    }
                },
                error => {
                    if (error) {
                        alert("Error: There was some error while updating room please try again...");
                    }
                })
        }
    }

    // to show form for edit Room Information  
    editRoomDetails(RoomId: number, Roomnumber: number, Floor: string, Numberofbeds: any, Numberofadults: any, Numberofchild: any, Roomtype: string, Description: string, FeePerDay: any, FeePerWeek: any, FeePerMonth: any, PercentageChange: any, Facilities: string) {
        this.AddTable = true;
        this.roomid = RoomId;
        this.roomnumber = Roomnumber;
        this.floor = Floor;
        this.numberofbeds = Numberofbeds;
        this.numberofadults = Numberofadults;
        this.numberofchild = Numberofchild;
        this.roomtype = Roomtype;
        this.description = Description;
        this.feeperday = FeePerDay;
        this.feeperweek = FeePerWeek;
        this.feepermonth = FeePerMonth;
        this.btntext = "Edit";
        //  this.getFacilities();
        this.facilitiesToEdit = Facilities.split(",");
        this.facilitiesComma = Facilities + ",";
    }

    updateHotelDetails(HotelId: number, Hotelname: string, Hotelfirstnumber: string, Hotelsecondnumber: string, Hotelcountry: string, Hotelstate: string, Hotelcity: string, Hotelzipcode: string, Hoteladdress: string, Hotellicensenumber: string, Hotelheadernotes: string, Hotelfooternotes: string, Hotelspecialnotes: string, ) {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        this.http.put(this.bseUrl + 'api/Room/UpdateHotel', JSON.stringify({ ID: HotelId, HotelName: Hotelname, HotelAddress: Hoteladdress, LisenceNo: Hotellicensenumber, PhoneNumberFirst: Hotelfirstnumber, PhoneNumberSecond: Hotelsecondnumber, Country: Hotelcountry, State: Hotelstate, City: Hotelcity, ZipCode: Hotelzipcode, HeaderNotes: Hotelheadernotes, FooterNotes: Hotelfooternotes, SpecialNotes: Hotelspecialnotes }),
            { headers: headers }).subscribe(
                response => {
                    this.getHotelDetails();

                }, error => {
                }
            );
    }

    filterrooms(Floor: string, Roomnumber: string, Availabilitystatus: boolean) {
        this.floorfilter = Floor;
        this.roomnumberfilter = Roomnumber;
        if (Availabilitystatus) {
            this.statustemp = "Available";
        }
        else {
            this.statustemp = "Booked";
        }
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        this.http.post(this.bseUrl + 'api/Room/FilterRooms', JSON.stringify({ RoomNo: Roomnumber, Floor: Floor, RoomType: this.RoomType, Status: this.statustemp, Facilities: this.facilitiesComma }),
            { headers: headers }).map((response: Response) => response.json()).subscribe(
                data => {
                    this.filterroomslist = data;

                }, error => {
                }
            );
    }

    RoomBookingbyAdmin(Fromdate: Date, Todate: Date, Bookingdate: Date, Firstname: string, Middlename: string, Lastname: string, Idcardnumber: string, Contactnumber: string, Advancepayment: any, City: string, Country: string, Address: string) {
        var flag = false;
        var headers = new Headers();
        headers.append('Content-Type', 'application/json; charset=utf-8');
        this.http.post(this.bseUrl + 'api/Room/RoomBookingByAdmin', JSON.stringify({ RoomID: this.RoomIdForBooking, FromeDateTime: Fromdate, ToDateTime: Todate, BookingDate: Bookingdate, FirstName: Firstname, MiddleName: Middlename, LastName: Lastname, IdCardNumber: Idcardnumber, ContactNumber: Contactnumber, AdvancePayment: Advancepayment, City: City, Country: Country, Address: Address, PaymentStatus: this.PType }),
            { headers: headers }).map((response: Response) => response.json()).subscribe(
            data => {
                if (this.statustemp == "Available") {
                    flag = true;
                }
                else {
                    flag = false;
                }
                alert("Room has been booked.");
                this.filterrooms(this.floorfilter, this.roomnumberfilter, flag);

            }, error => {
                alert("An error has occurred try again later");
                }
            );
    }

    //filterrooms(Floor: string, Roomnumber: string, Availabilitystatus: boolean) {
    //    if (Availabilitystatus) {
    //        this.statustemp = "True";
    //    }
    //    else {
    //        this.statustemp = "False";
    //    }
    //    //var headers = new Headers();
    //    //headers.append('Content-Type', 'application/json; charset=utf-8');

    //    //this._hotelService.getFilteredRooms(JSON.stringify({ RoomNo: Roomnumber, Floor: Floor, RoomType: this.RoomType, Status: this.statustemp, Facilities: this.facilitiesComma })).subscribe(
    //    //    data => this.filterroomslist = data
    //    //)

    //    var headers = new Headers();
    //    headers.append('Content-Type', 'application/json; charset=utf-8');
    //    this.http.post(this.bseUrl + 'api/Room/FilterRooms', JSON.stringify({ RoomNo: Roomnumber, Floor: Floor, RoomType: this.RoomType, Status: this.statustemp, Facilities: this.facilitiesComma }),
    //        { headers: headers })
    //        .map(res => this.filteredroomlist = res.json())
    //        .catch(this.errorHandler);
    //}
    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

    RoomIDBooking(roomId: number, rentPerDay: number) {
        this.RoomIdForBooking = roomId;
        this.FeeCalFix = rentPerDay;
    }
    // Add rooms final post start

    public fileList: FileList;
    fileChange(event: any) {
        let fileList: FileList = event.target.files;
        this.fileList = fileList;
    }
    // Add rooms final post end

}

interface RoomData {
    roomid: number;
    floor: string;
    roomno: string;
    noofbed: number;
    noofadult: number;
    noofchild: number;
    roomtype: string;
    status: string;
    feeperday: any;
    feeperweek: any;
    feepermonth: any;
    percentagechange: number;
    roomdescription: string;
    image: string;
    facilities: string;
}

interface Hotel {
    id: number,
    hotelname: string,
    hoteladdress: string,
    lisenceno: string,
    logo: string,
    phonenumberfirst: string,
    phonenumbersecond: string,
    country: string
    state: string;
    city: string;
    zipcode: string;
    headernotes: string;
    footernotes: string;
    specialnotes: string;
}

interface BookedRoom {
    id: number,
    roomnumber: string,
    customername: string,
    fromdate: any,
    todate: any,
    remainingdays: number
}

interface IFilterRoomsData {
    customerName?: any;
    fromeDateTime: Date;
    toDateTime: Date;
    remainingDays: number;
    roomPriceID: number;
    feePerDay: number;
    feePerWeek: number;
    feePerMonth: number;
    percentageChange: number;
    roomPriceField1?: any;
    roomPriceField2?: any;
    roomPriceField3?: any;
    roomPriceField4?: any;
    roomPriceField5?: any;
    roomPriceField6: number;
    roomFacilitID: number;
    facilityName?: any;
    iFacilityAvailable: boolean;
    roomFacilityField1?: any;
    roomFacilityField2?: any;
    roomFacilityField3?: any;
    roomFacilityField4?: any;
    roomFacilityField5?: any;
    roomFacilityField6: number;
    roomID: number;
    floor: string;
    roomNo: string;
    noOfBed: number;
    noOfAdult: number;
    noOfChild: number;
    roomType: string;
    status: string;
    description: string;
    roomField1?: any;
    roomField2?: any;
    roomField3?: any;
    roomField4?: any;
    roomField5?: any;
    roomField6: number;
    cb: number;
    mb: number;
    db: number;
    cd: Date;
    md: Date;
    dd: Date;
    facilities: string;
    image: string;
}

interface IFacilities {
    facilityid: number;
    facilityname: string;
    isavailable: boolean;
}