export class Timesheet {
    id: number;
    teamMemberId: number;
    clockIn: Date;
    clockOut?: Date;
    autoClockOut: boolean;
    createdOn: Date;
    updatedOn: Date;

    get hours() {
        if(!this.clockOut) {
            return 0;
        }
        return Math.abs(this.clockIn.getTime() - this.clockOut.getTime()) / 36e5;
    }

    constructor(obj: any) {
        if(obj) {
            Object.keys(obj).forEach(key => {
                this[key] = obj[key];
            });

            this.clockIn = new Date(this.clockIn);
            
            if(this.clockOut) {
                this.clockOut = new Date(this.clockOut);
            }
        }
    }
}