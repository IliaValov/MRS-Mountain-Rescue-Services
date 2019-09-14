import jwtDecode from 'jwt-decode';
import MissionLogModel from '../models/MissionLogModel';
import  AuthService  from './AuthService';

class MissionLogService {

    authService = new AuthService();

    addMissionLog(missionLog) {
        return fetch('https://localhost:44358/api/missionlogs/addmissionlog', {
            method: 'post',
            headers: {
                'Authorization': 'Bearer '+ this.authService.getToken(),
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(missionLog)
        })
            .then(res => res)
            .catch(error => {
                Promise.reject(error);
            })
    }

    getAllMissionLogs() {
        return fetch('https://localhost:44358/api/missionlogs/getallmissionlogsbyuser', {
            method: 'get',
            headers: {
                'Authorization': 'Bearer '+ this.authService.getToken(),
                'Content-Type': 'application/json'
            },
        })
            .then(res => res.json())
            .then(response => {
                return response;
            })
            .catch(error => {
                Promise.reject(error);
            })
    }

}

export default MissionLogService