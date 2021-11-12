import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { NgSelectModule } from "@ng-select/ng-select";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

import { RemoveWrapperDirective } from "./directives/remove-wrapper.directive";
import { CustomComponentValidateDirective } from "./directives/custom-component-validate.directive";

import { CustomTextFieldComponent } from "./components/custom-textfield/custom-textfield.component";
import { CustomTextAreaComponent } from "./components/custom-textarea/custom-textarea.component";
import { CustomCheckComponent } from "./components/custom-check/custom-check.component";
import { CustomComboComponent } from "./components/custom-combo/custom-combo.component";
import { CustomRadioComponent } from "./components/custom-radio/custom-radio.component";
import { CustomHiddenComponent } from "./components/custom-hidden/custom-hidden.component";
import { CustomDateComponent } from "./components/custom-date/custom-date.component";

@NgModule({
  exports: [
    RemoveWrapperDirective,
    CustomComponentValidateDirective,

    CustomTextFieldComponent,
    CustomTextAreaComponent,
    CustomCheckComponent,
    CustomComboComponent,
    CustomRadioComponent,
    CustomHiddenComponent,
    CustomDateComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NgSelectModule,
    NgbModule
  ],
  declarations: [
    RemoveWrapperDirective,
    CustomComponentValidateDirective,

    CustomTextFieldComponent,
    CustomTextAreaComponent,
    CustomCheckComponent,
    CustomComboComponent,
    CustomRadioComponent,
    CustomHiddenComponent,
    CustomDateComponent
  ]
})
export class CustomComponentsModule { }
