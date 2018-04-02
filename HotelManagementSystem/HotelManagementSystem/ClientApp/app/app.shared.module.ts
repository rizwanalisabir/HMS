import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { HotelService } from './services/hotel.service';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MainPageComponent } from './components/mainpage/mainpage.component';
import { BrowserModule } from '@angular/platform-browser';
import { FacilityComponent } from './components/facility/facility.component';
import { FacilityService } from './components/facility/facility.service';
import { NumberPickerComponent } from './components/angular2-number-picker/angular2-number-picker.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoomBookingComponent } from './components/roombooking/roombooking.component';


@NgModule({
    declarations: [
        AppComponent,
        FetchDataComponent,
        HomeComponent,
        HeaderComponent,
        FooterComponent,
        MainPageComponent,
        FacilityComponent,
        NumberPickerComponent,
        RoomBookingComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'mainpage', component: MainPageComponent },
            { path: 'facility', component: FacilityComponent },
            { path: 'header', component: HeaderComponent },
            { path: 'roombooking', component: RoomBookingComponent }
            //{ path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [HotelService, FacilityService]
})
export class AppModuleShared {
}
