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
import { CustomBaseComponent } from "../../../custom/components/custom-base-component/CustomBaseComponent";
import { CustomComponentValidateDirective } from "../../../custom/directives/custom-component-validate.directive";
import Utils from "../../../shared/helpers/Utils";
import { GenericResponse } from "../../../shared/models/common/GenericResponse";
import { RolesService } from "../../../shared/services/roles.service";

@Component({
  selector: "app-roles-edit",
  templateUrl: "./roles-edit.component.html",
  styleUrls: ["./roles-edit.component.scss"]
})
export class RolesEditComponent extends CustomBaseComponent implements OnInit {
  active = 1;
  formValidate: FormGroup;

  @ViewChildren(CustomComponentValidateDirective) validators: QueryList<CustomComponentValidateDirective>;

  constructor(
    private route: ActivatedRoute,

    private service: RolesService
  ) {
    super();
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.editData();
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
          Utils.IsValidJsonObject(response.data)) {
        this.populateModel(response.data, this.validators);
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
    model.normalizedName = model.name.toUpperCase();
    model.concurrencyStamp = this.createGuid();

    this.service.post(model).subscribe((response: GenericResponse<any>) => {
      if (response &&
        response.status === 1 &&
        Utils.IsValidJsonObject(response.data)) {
        this.setValue("Id", response.data.id, this.validators);
        this.setValue("NormalizedName", response.data.normalizedName, this.validators);
        this.setValue("ConcurrencyStamp", response.data.concurrencyStamp, this.validators);

        alert("La información se guardó correctamente");
      }
    },
    (error: any) => {

    },
    () => {

    });
  }

  private createGuid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}
