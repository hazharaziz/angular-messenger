export const log = (message?: any, optionalParams?: any[]): void => {
  if (optionalParams) {
    console.log(JSON.stringify(message, undefined, 2), optionalParams);
  } else {
    console.log(JSON.stringify(message, undefined, 2));
  }
};
