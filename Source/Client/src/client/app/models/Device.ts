module app.models {

    export interface IDeviceStateInfo {
        id: number;
        name: string;
        stateNumber: number;
    }

    export interface IDeviceInfo {
        id: number;
        name: string;
        possibleStates: Array<IDeviceStateInfo>;
        currentStateId: number;
        nextEvent: string;
        smallImageUrl: string;
        largeImageUrl: string;
    }
}