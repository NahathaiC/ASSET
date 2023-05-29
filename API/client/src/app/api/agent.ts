import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/Routes";

const sleep = () => new Promise(resolve => setTimeout(resolve, 500));

axios.defaults.baseURL = 'http://localhost:5050/api/';

const responsesBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(async response => {
    await sleep();
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
    get: (url: string) => axios.get(url).then(responsesBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responsesBody),
    put: (url: string, body:{}) => axios.put(url, body).then(responsesBody),
    delete: (url: string) => axios.delete(url).then(responsesBody),
}

const Catalog = {
    list: () => requests.get('PR'),
    details: (id: number) => requests.get(`PR/${id}`)
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