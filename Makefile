.PHONY: push
push:
	okteto build -t okteto/hello-world:aspnetcore-dev --target dev .
	okteto build -t okteto/hello-world:aspnetcore .
