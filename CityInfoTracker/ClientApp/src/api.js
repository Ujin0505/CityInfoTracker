import axios from "axios";

const instance = axios.create({
    baseURL: 'https://localhost:5001/'
})

export const getCityInfo = async (id) =>{
    try
    {
        const url = `cityInfo/${id}`;
        const responce = await instance.get(url);
        if(responce.status === 200){
            return responce.data;
        }
    }
    catch (e) {
        throw e;
    }
}

