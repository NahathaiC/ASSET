import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/Routes";
import { PaginatedResponse } from "../models/pagination";

const sleep = () => new Promise(resolve => setTimeout(resolve, 500));

axios.defaults.baseURL = 'http://localhost:5050/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(async response => {
    await sleep();
    const pagination = response.headers['pagination'];
    if (pagination) {
        response.data = new PaginatedResponse(response.data, JSON.parse(pagination));
        return response;
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    switch (status) {
      case 400:
        if (data.errors) {
                const modelStateError: string[] = [];
                for (const key in data.errors) {
                    if (data.errors[key]){
                        modelStateError.push(data.errors[key])
                    }
                }
                throw modelStateError.flat();
            }
        toast.error(data.title);
        break;

      case 401:
        toast.error(data.title);
        break;

      case 500:
        router.navigate('/server-error',{state: {error: data}});
        break;

      default:
        break;
    }
    return Promise.reject(error.response);
  }
);

const requests = {
    get: (url: string, params?: URLSearchParams) => axios.get(url, {params}).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body:{}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const Catalog = {
    list: (params: URLSearchParams) => requests.get('PR', params),
    details: (id: number) => requests.get(`PR/${id}`),
    fetchFilter: () => requests.get('PR/filters')
}

const TestErrors = {
    get400Error: () => requests.get('buggy/bad-request'),
    get401Error: () => requests.get('buggy/unauthorised'),
    get404Error: () => requests.get('buggy/not-found'),
    get500Error: () => requests.get('buggy/server-error'),
    getValidationError: () => requests.get('buggy/validation-error'),
}

const Account = {
  login: (values: any) => requests.post('Account/login', values),
  register: (values: any) => requests.post('Account/register', values),
  currentUser: () => requests.get('Account/currentUser'),
}

const agent = {
    Catalog,
    TestErrors,
    Account
}

export default agent;