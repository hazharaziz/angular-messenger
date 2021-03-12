import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidator {
  static ValidateString(min: number, max: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      let result: any = null;
      if (control.value !== undefined) {
        let text: string = control.value.toString();
        if (text.trim().length < min || text.trim().length > max) {
          result = { lengthRange: false };
        }
      }
      return result;
    };
  }
}
