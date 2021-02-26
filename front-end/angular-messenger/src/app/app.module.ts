import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/screens/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InputFormComponent } from './components/shared/input-form/input-form.component';
import { AppRoutingModule } from './app-routing/app-routing.module';

@NgModule({
  declarations: [AppComponent, LoginComponent, InputFormComponent],
  imports: [
    BrowserModule,
    StoreModule.forRoot({}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
