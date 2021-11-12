import {
    Component,
    OnInit,
    QueryList,
    ViewChildren
} from "@angular/core";
import {
    FormGroup
} from "@angular/forms";
import {
    ActivatedRoute, Params
} from "@angular/router";
import { forkJoin } from "rxjs";
import { CustomBaseComponent } from "../../../custom/components/custom-base-component/CustomBaseComponent";
import { CustomComboComponent } from "../../../custom/components/custom-combo/custom-combo.component";
import { CustomComponentValidateDirective } from "../../../custom/directives/custom-component-validate.directive";
import Utils from "../../../shared/helpers/Utils";
import { GenericResponse } from "../../../shared/models/common/GenericResponse";

import { RolesService } from "../../../shared/services/roles.service";


import { UsersService } from "../../../shared/services/users.service";

@Component({
  selector: "app-users-edit",
  templateUrl: "./users-edit.component.html",
  styleUrls: ["./users-edit.component.scss"]
})
export class UsersEditComponent extends CustomBaseComponent implements OnInit {
  active = 1;
  formValidate: FormGroup;

  @ViewChildren(CustomComponentValidateDirective) validators: QueryList<CustomComponentValidateDirective>;

  constructor(
    private route: ActivatedRoute,
    private rolesService: RolesService,

    private service: UsersService
  ) {
    super();
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    forkJoin(
      this.rolesService.get()
    ).subscribe((response: Array<any>) => {

      const [roles] = response;

      if (roles &&
          roles.status === 1 &&
          Utils.IsValidArray(roles.data)) {
        this.setDataSource("Role", roles.data, this.validators);
      }
    },
    (error: any) => {

    },
    () => {
      this.editData();
    });
  }

  editData() {
    this.route.params.subscribe((params: Params) => {
      const id = params["id"];
      if (id) {
        this.getEntity(id);
      }
    });
  }

  getEntity(id: any) {
    this.service.getById(id).subscribe((response: GenericResponse<any>) => {
      if (response &&
          response.status === 1 &&
          Utils.IsValidJsonObject(response.data) &&
          Utils.IsValidJsonObject(response.data.user)) {
        this.populateModel(response.data.user, this.validators);

        if (Utils.IsValidArray(response.data.roles) && response.data.roles.length > 0) {
          const role = response.data.roles[0];

          this.setValue("Role", role.id, this.validators);
        }
      }
    },
    (error: any) => {

    },
    () => {

    });
  }

  ngAfterViewInit() {
    const validators: any = {};

    this.validators.forEach((x: CustomComponentValidateDirective) => {
      if (Utils.IsValidStringNotEmpty(x.component.name) && x.component.control) {
        validators[x.component.name] = x.component.control;
      }
    });

    this.formValidate = new FormGroup(validators);

    this.getData();
  }

  save() {
    const isValid = this.formValidate.valid;
    if (!isValid) {
      return;
    }

    const model = this.generateModel(this.validators);

    const componentEntityDirective = this.validators.find((x: CustomComponentValidateDirective) => Utils.IsValidStringNotEmpty(x.component.name) && Utils.FirstCharToLowerCase(x.component.name) === Utils.FirstCharToLowerCase("Role"));
    const roleComponent = componentEntityDirective.component as CustomComboComponent;

    model.role = roleComponent.getLabel();

    this.service.post(model).subscribe((response: GenericResponse<any>) => {
      if (response &&
        response.status === 1 &&
        Utils.IsValidJsonObject(response.data)) {
        this.setValue("Id", response.data.id, this.validators);
        alert("La información se guardó correctamente");
      }
    },
    (error: any) => {

    },
    () => {

    });
  }
}
