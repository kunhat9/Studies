"use strict"


/**
 * Responsible for all HTTP requests.
 */
const http = {
    // eslint-disable-next-line
    async request(method, url, data, options = {}) {
        try {
            const apiUrl = url
            const type = options.type || 'json'

            const headers = {
                credentials: 'include',
                headers: { "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8" },
            }
            
            const init = {
                method: method.toUpperCase(),
                headers: Object.assign(headers, options.headers || {}),
                body:data
            }
            
            
            const response = await fetch(apiUrl, init)
            

            if (type === 'json') {
                return await response.json()
            }

            if (type === 'blob') {
                return await response.blob()
            }

            return await response.text()
        } catch (e) {
            return {
                Data:null
            }
        }
    },
    get(url, options) {
        return this.request('get', url, {}, options)
    },
    post(url, data, options) {
        return this.request('post', url, data, options)
    },
    put(url, data, options) {
        return this.request('put', url, data, options)
    },
    delete(url, data = {}, options) {
        return this.request('delete', url, data, options)
    },
}

