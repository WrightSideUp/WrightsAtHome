module app.models {

    export class Utils {
        public static copyProperties(source: any, target: any): void {
            for (var prop in source) {
                if (source.hasOwnProperty(prop)) {
                    target[prop] = source[prop];
                }
            }
        }
    }
}
