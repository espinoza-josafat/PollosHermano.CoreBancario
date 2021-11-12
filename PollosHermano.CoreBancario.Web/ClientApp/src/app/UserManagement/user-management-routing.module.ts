import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { UsersListComponent } from "./Users/List/users-list.component";
import { UsersEditComponent } from "./Users/Edit/users-edit.component";
import { RolesListComponent } from "./Roles/List/roles-list.component";
import { RolesEditComponent } from "./Roles/Edit/roles-edit.component";

const routes: Routes = [
  {
    path: "",
    children: [


      {
        path: "Users/List",
        component: UsersListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Users/Edit",
        component: UsersEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Users/Edit/:id",
        component: UsersEditComponent,
        data: {
          title: "edit"
        }
      },

      {
        path: "Roles/List",
        component: RolesListComponent,
        data: {
          title: "list"
        }
      },
      {
        path: "Roles/Edit",
        component: RolesEditComponent,
        data: {
          title: "edit"
        }
      },
      {
        path: "Roles/Edit/:id",
        component: RolesEditComponent,
        data: {
          title: "edit"
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserManagementRoutingModule { }
