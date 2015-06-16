module app.models {

    export interface ISensorReading {
        id: number;
        name: string;
        sensorType: string;
        reading: number;
        smallImageUrl: string;
        largeImageUrl: string;

        readingDisplayText(): string;
    }

    export function SensorReadingMixer() {

        return {

                mixInto: function(obj): ISensorReading {
                    return angular.extend(obj, this);
                },
            
                readingDisplayText: function() {

                    if (this.sensorType === "Temperature")
                        return this.reading + "&deg;";
                    else if (this.sensorType === "LightLevel")
                        return this.reading + " lux";
                    else {
                        if (this.reading >= 1)
                            return "Daytime";
                        else
                            return "Night-time";
                    }
                }
        };
    }
}