import { log } from './logger';

export class DateTime {
  static getDate(dateTime: string): string {
    let date = new Date(dateTime);
    return date.toLocaleDateString(undefined, {
      year: 'numeric',
      month: 'short',
      day: '2-digit'
    });
  }

  static getTime(dateTime: string): string {
    let date = new Date(dateTime);
    return date.toLocaleTimeString(undefined, {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    });
  }
}
