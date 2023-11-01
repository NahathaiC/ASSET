import * as yup from 'yup';

export const validationSchema = yup.object({
    id: yup.number().required(),
    userName: yup.string().required(),
    email: yup.string().required(),
    position: yup.string().required(),
    department: yup.string().required(),
    section: yup.string().required(),
    phone: yup.string().required(),
})