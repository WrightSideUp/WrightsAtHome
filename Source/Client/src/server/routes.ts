/*jshint node:true*/

'use strict';

import express = require('express');

var router = express.Router();
import four0four = require('./utils/notfound');
var notFoundMiddleware = four0four.notFoundMiddleware;
var send404 = four0four.send404;

import data = require('./data');

router.get('/people', getPeople);
router.get('/person/:id', getPerson);
router.get('/*', notFoundMiddleware);

module.exports = router;

//////////////

//EG TODO: find type for next argument
function getPeople(req: express.Request, res: express.Response, next: any) {
    res.status(200).send(data.getPeople());
}

function getPerson(req: express.Request, res: express.Response, next: any) {
    var id = +req.params.id;
    var person = data.getPeople().filter(function(p) {
        return p.id === id;
    })[0];

    if (person) {
        res.status(200).send(person);
    } else {
        send404(req, res, 'person ' + id + ' not found');
    }
}
