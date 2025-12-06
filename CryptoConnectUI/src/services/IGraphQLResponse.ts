export interface IGraphQLResponse<T> {
    data: T;
    errors?: Array<{ message: string }>;
}