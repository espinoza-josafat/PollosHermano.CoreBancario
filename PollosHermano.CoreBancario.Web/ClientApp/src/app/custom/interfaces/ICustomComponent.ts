import { FormControl } from "@angular/forms";

export interface ICustomComponent {
  id: string;
  control: FormControl;
  name: string;
  metadata: any;
  getType(): string;
}
