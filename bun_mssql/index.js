    function query(sql, resultFunc)
    {
        var Connection = require('tedious').Connection;  
        var config = {  
            server: '192.168.0.67',  //update me
            port:1433,
            authentication: {
                type: 'default',
                options: {
                    userName: 'sa', //update me
                    password: 'cAs2ptUjbEyCP%m^GKe9'  //update me
                }
            },
            options: {
                // If you are on Microsoft Azure, you need encryption:
                encrypt: false,
                //trustServerCertificate: true,
                database: 'burnoutados'  //update me
            }
        };  
        var connection = new Connection(config);  
        connection.on('connect', function(err) {  
            // If no error, then good to proceed.
            console.log("connecting");  
            if(err)
            {
                console.log(err);
            }
            else
            {
                console.log("Connected"); 
                executeStatement(sql, resultFunc);
            }
                
             
        });


        var Request = require('tedious').Request;  
        var TYPES = require('tedious').TYPES;  
      
        function executeStatement(sql, resultFunc) {  
            var request = new Request(sql, function(err) {  
            if (err) {  
                console.log(err);}  
            });  


            
            let results=[];  
            request.on('row', function(columns) {  
                let result ={};
                columns.forEach(function(column) { 
                    //console.log(column);
                    result[column.metadata.colName]= column.value;

                });  
                results.push(result);

            });  
      
            request.on('done', function(rowCount, more) {  
                console.log(rowCount + ' rows returned'); 
                console.log(more); 

            });  
            
            // Close the connection after the final event emitted by the request, after the callback passes
            request.on("requestCompleted", function (rowCount, more) {
                connection.close();
                console.log(rowCount + ' rows returned'); 
                console.log(more); 
                resultFunc(results);
            });
            connection.execSql(request);  
        } 
        
        connection.connect();
    }


    query("select * from Users;", function(result)
    {
        console.log(result);
    });