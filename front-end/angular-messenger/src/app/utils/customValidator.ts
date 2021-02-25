import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidator {
  static ValidateString(min: number, max: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      let result: any = null;
      if (control.value !== undefined) {
        let text: string = control.value.toString();
        if (text.length < min || text.length > max) {
          result = { lengthRange: false };
        }
      }
      return result;
    };
  }
}
