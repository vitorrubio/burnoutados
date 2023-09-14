import tedious from 'tedious' 


    function execQuery(sql, resultFunc, errorFunc)
    {
        
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
        var connection = new tedious.Connection(config);  
        connection.on('connect', function(err) {  
            // If no error, then good to proceed.
            console.log("connecting");  
            if(err)
            {
                console.log(err);
                errorFunc(err);
            }
            else
            {
                console.log("Connected"); 
                executeStatement();
            }
                
             
        });


        
        //var TYPES = require('tedious').TYPES;  
      
        function executeStatement() {  
            var request = new tedious.Request(sql, function(err) {  
                if (err) {  
                    console.log(err);
                    errorFunc(err);
                }  
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




    export const  query = async (sql) => 
    {
        return new Promise((resolve, reject) => 
        {
            execQuery(sql, resolve, reject);
        });
    }


    let result = await query("select * from Users;");
    console.log(result);