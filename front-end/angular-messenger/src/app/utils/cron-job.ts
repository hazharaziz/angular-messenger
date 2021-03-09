import cron from 'cron';

export class CronJob {
  static start(pattern: string, callback: (...args: any[]) => void) {
    let cronJob = cron.CronJob;
    let job = new cronJob(pattern, callback);
    job.start();
  }
}
