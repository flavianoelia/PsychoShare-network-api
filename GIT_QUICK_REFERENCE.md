# Git Quick Reference - PsychoShare Team

## 🚀 Comandos Más Usados

### Setup Inicial
```bash
# Clonar repo
git clone https://github.com/flavianoelia/PsychoShare-network-api.git
cd PsychoShare-network-api

# Configurar usuario (una vez)
git config --global user.name "Tu Nombre"
git config --global user.email "tu-email@example.com"
```

### Flujo Diario
```bash
# 1. Actualizar development
git checkout development
git pull origin development

# 2. Crear nueva feature
git checkout -b feature/mi-nueva-caracteristica

# 3. Hacer cambios y commit
git add .
git commit -m "feat: add new feature description"

# 4. Subir branch
git push origin feature/mi-nueva-caracteristica

# 5. Crear PR en GitHub hacia 'development'
```

### Comandos de Rescate 🆘

```bash
# Deshacer último commit (mantener cambios)
git reset --soft HEAD~1

# Deshacer cambios no committeados
git checkout -- .

# Ver estado actual
git status

# Ver branches
git branch -a

# Cambiar de branch
git checkout nombre-branch

# Eliminar branch local
git branch -d feature/mi-branch

# Eliminar branch remoto
git push origin --delete feature/mi-branch
```

### PR Management

| Situación | Solución |
|-----------|----------|
| PR apunta a branch incorrecto | Cambiar target en GitHub UI |
| PR duplicado | Cerrar con comentario explicativo |
| Branch equivocado | Crear nuevo PR desde branch correcto |
| Quiero eliminar PR | Cerrar PR + eliminar branch |

### ⚠️ Reglas de Oro

1. **NUNCA** hacer push directo a `main`
2. **SIEMPRE** crear PR hacia `development`  
3. **SIEMPRE** usar nombres descriptivos en inglés
4. **SIEMPRE** hacer pull antes de crear nueva branch
5. **NUNCA** hacer rebase en branches compartidas

### 📞 ¿Necesitas Ayuda?

```bash
# Si algo sale mal:
git status  # Ver qué pasó
git log --oneline -5  # Ver últimos commits  
git reflog  # Ver historial completo (para recuperar)
```

**Recuerda**: Es mejor preguntar que romper el repositorio 😅