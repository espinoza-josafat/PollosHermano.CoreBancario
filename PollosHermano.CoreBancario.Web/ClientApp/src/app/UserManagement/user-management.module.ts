import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { NgxDatatableModule } from "@swimlane/ngx-datatable";

import { UserManagementRoutingModule } from "./user-management-routing.module";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { CustomFormsModule } from "ngx-custom-validators";
import { ArchwizardModule } from "angular-archwizard";
import { UiSwitchModule } from "ngx-ui-switch";
import { NgSelectModule } from "@ng-select/ng-select";
import { TagInputModule } from "ngx-chips";
import { QuillModule } from "ngx-quill";
import { MatchHeightModule } from "../shared/directives/match-height.directive";

import { CustomComponentsModule } from "../custom/custom-components.module";

import { UsersListComponent } from "./Users/List/users-list.component";
import { UsersEditComponent } from "./Users/Edit/users-edit.component";
import { RolesListComponent } from "./Roles/List/roles-list.component";
import { RolesEditComponent } from "./Roles/Edit/roles-edit.component";

@NgModule({
  imports: [
    CommonModule,
    NgxDatatableModule,
    UserManagementRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ArchwizardModule,
    CustomFormsModule,
    MatchHeightModule,
    NgbModule,
    UiSwitchModule,
    QuillModule.forRoot(),
    NgSelectModule,
    TagInputModule,
    CustomComponentsModule
  ],
  declarations: [
    UsersListComponent,
    UsersEditComponent,
    RolesListComponent,
    RolesEditComponent
  ]
})
export class UserManagementModule { }
