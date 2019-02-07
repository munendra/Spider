import { Error } from './Error';

export class ApiResponse {
    statusCode: number;
    errors: Error;
    data: any;
}


