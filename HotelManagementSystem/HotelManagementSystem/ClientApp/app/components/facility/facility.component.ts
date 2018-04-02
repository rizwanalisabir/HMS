import { Component, Input, Inject, ViewChild, ElementRef } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { FacilityService } from '../facility/facility.service'
import { Router } from '@angular/router';
import { forEach } from '@angular/router/src/utils/collection';
import { Observable } from 'rxjs/Observable';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
    selector: 'facility',
    templateUrl: './facility.component.html',
    providers: [FacilityService]
})

export class FacilityComponent {

    public facilities: IFacilities[];
    public facilityid: string = "0";
    public facilityname: string = "";
    public isavailable: boolean = false;
    public btnFacilitytext: string = "Add";
    public bseUrl: string = "";
    public AddFacilityForm: boolean = false;
    ShowNewFacility() {
        this.AddFacilityForm = true;
        this.btnFacilitytext = "Add";
        this.facilityid = "0";
        this.facilityname = "";
        this.isavailable = false;
        this.bseUrl = "";
    }

    HideForm() {
        this.AddFacilityForm = false;
    }

    constructor(public http: Http, private _router: Router, private _facilityService: FacilityService) {
        this.getFacilities();
    }

    getFacilities() {
        this._facilityService.getFacilities().subscribe(
            data => this.facilities = data
        )
    }

    delete(facilitiid: number) {
        var ans = confirm("Do you want to delete this facility?");
        if (ans) {
            this._facilityService.DeleteFacility(facilitiid).subscribe((data) => {
                this.getFacilities();
            }, error => console.error(error))
        }
    }

    AddFacility(FacilityId: number, FacilityName: string, IsAvailable: boolean) {
        let formData: FormData = new FormData();
        formData.append('ID', FacilityId.toString());
        formData.append('FacilityName', FacilityName);
        if (IsAvailable) {
            formData.append('IsAvailable', 'True');
        }
        else {
            formData.append('IsAvailable', 'False');
        }
        let headers = new Headers();
        let options = new RequestOptions({ headers: headers });
        if (FacilityId.toString() == "0") {
            this.http.post(this.bseUrl + 'api/Facility/Create', formData, options)
                .map(res => res.json())
                .subscribe
                (
                data => {
                    if (data == true) {
                        this.getFacilities();
                        this.HideForm();
                        alert("Success: Facility was saved...");
                    }
                    else {
                        alert("Error: There was some error while adding new facility please try again...");
                    }
                },
                error => {
                    if (error) {
                        alert("Error: There was some error while adding new facility please try again...");
                    }
                })
        }
        else {
            this.http.put(this.bseUrl + 'api/Facility/Edit', formData, options)
                .map(res => res.json())
                .subscribe
                (
                data => {
                    if (data == true) {
                        this.getFacilities();
                        this.HideForm();
                        alert("Success: Facility was updated...");
                    }
                    else {
                        alert("Error: There was some error while updating facility please try again...");
                    }
                },
                error => {
                    if (error) {
                        alert("Error: There was some error while updating facility please try again...");
                    }
                })
        }
    }
    showInEditMode(FacilityId: string, FacilityName: string, IsAvailable: boolean) {
        this.AddFacilityForm = true;
        this.facilityid = FacilityId;
        this.facilityname = FacilityName;
        this.isavailable = IsAvailable;
        this.btnFacilitytext = "Edit";
    }

    private fileList: FileList;

    fileChange(event: any) {
        let fileList: FileList = event.target.files;
        this.fileList = fileList;
    }
}

interface IFacilities {
    facilityid: number;
    facilityname: string;
    isavailable: boolean;
}